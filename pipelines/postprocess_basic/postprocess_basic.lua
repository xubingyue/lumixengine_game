_postprocess_slot = "post_tonemapping"

dof_focal_distance = 0
dof_focal_range = 10
dof_enabled = true
film_grain_enabled = true
grain_amount = 0.02
grain_size = 1.6
enabled = true
max_dof_blur = 1
dof_clear_range = 75
dof_near_multiplier = 100
fxaa_enabled = true
vignette_radius = 0.5
vignette_softness = 0.35
vignette_enabled = true
bloom_enabled = false
bloom_cutoff = 1.0


local pipeline_env = nil

local bloom_debug = false
local bloom_debug_fullscreen = false
local bloom_blur = true

function initPostprocessBasic(pipeline, ctx)
	addFramebuffer(pipeline,  "dof", {
		width = 1024,
		height = 1024,
		size_ratio = { 0.5, 0.5},
		renderbuffers = {
			{ format = "rgba16f" },
		}
	})

	addFramebuffer(pipeline,  "dof_blur", {
		width = 1024,
		height = 1024,
		size_ratio = { 0.5, 0.5},
		renderbuffers = {
			{ format = "rgba8" },
		}
	})

	addFramebuffer(pipeline, "postprocess_result", {
		width = 1024,
		height = 1024,
		size_ratio = {1, 1},
		renderbuffers = {
			{ format = "rgba8" },
		}
	})
	
	ctx.bloom_cutoff = createVec4ArrayUniform(pipeline, "u_bloomCutoff", 1)
	ctx.grain_amount_uniform = createVec4ArrayUniform(pipeline, "u_grainAmount", 1)
	ctx.grain_size_uniform = createVec4ArrayUniform(pipeline, "u_grainSize", 1)
	ctx.downsample_material = Engine.loadResource(g_engine, "pipelines/common/downsample.mat", "material")
	ctx.ppbasic_material = Engine.loadResource(g_engine, "pipelines/postprocess_basic/ppbasic.mat", "material")
	ctx.fxaa_material = Engine.loadResource(g_engine, "pipelines/postprocess_basic/fxaa.mat", "material")
	ctx.hdr_buffer_uniform = createUniform(pipeline, "u_hdrBuffer")
	ctx.dof_buffer_uniform = createUniform(pipeline, "u_dofBuffer")
	ctx.fxaa_buffer_uniform = createUniform(pipeline, "u_fxaaBuffer")
	ctx.dof_focal_distance_uniform = createVec4ArrayUniform(pipeline, "focal_distance", 1)
	ctx.dof_focal_range_uniform = createVec4ArrayUniform(pipeline, "focal_range", 1)
	ctx.max_dof_blur_uniform = createVec4ArrayUniform(pipeline, "max_dof_blur", 1)
	ctx.dof_near_multiplier_uniform = createVec4ArrayUniform(pipeline, "dof_near_multiplier", 100)
	ctx.dof_clear_range_uniform = createVec4ArrayUniform(pipeline, "clear_range", 0)
	ctx.vignette_uniform = createVec4ArrayUniform(pipeline, "u_vignette", 1)
end

function postprocessBasic(pipeline, ctx, camera_slot)
	bloom(ctx, pipeline)
		
	setMaterialDefine(pipeline, ctx.ppbasic_material, "FILM_GRAIN", film_grain_enabled)
	setMaterialDefine(pipeline, ctx.ppbasic_material, "DOF", dof_enabled)	
	setMaterialDefine(pipeline, ctx.ppbasic_material, "VIGNETTE", vignette_enabled)	
	if dof_enabled then
		newView(pipeline, "dof", "dof")
			disableDepthWrite(pipeline)
			setPass(pipeline, "MAIN")
			bindFramebufferTexture(pipeline, "hdr", 0, ctx.texture_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
			drawQuad(pipeline, 0, 0, 1, 1, ctx.screen_space_material)

		newView(pipeline, "blur_dof_h", "dof_blur")
			setPass(pipeline, "BLUR_H")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "dof", 0, ctx.shadowmap_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
			drawQuad(pipeline, 0, 0, 1, 1, ctx.blur_material)
			enableDepthWrite(pipeline)

		newView(pipeline, "blur_dof_v", "dof")
			setPass(pipeline, "BLUR_V")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "dof_blur", 0, ctx.shadowmap_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
			drawQuad(pipeline, 0, 0, 1, 1, ctx.blur_material);
			enableDepthWrite(pipeline)

		newView(pipeline, "ppbasic", "postprocess_result")
			setPass(pipeline, "MAIN")
			disableBlending(pipeline)
			applyCamera(pipeline, camera_slot)
			disableDepthWrite(pipeline)
			clear(pipeline, CLEAR_COLOR | CLEAR_DEPTH, 0x00000000)

			bindFramebufferTexture(pipeline, "hdr", 0, ctx.hdr_buffer_uniform)
			bindFramebufferTexture(pipeline, "dof", 0, ctx.dof_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
			bindFramebufferTexture(pipeline, "hdr", 1, ctx.depth_buffer_uniform)

			setUniform(pipeline, ctx.dof_focal_distance_uniform, {{dof_focal_distance, 0, 0, 0}})
			setUniform(pipeline, ctx.dof_focal_range_uniform, {{dof_focal_range, 0, 0, 0}})
			setUniform(pipeline, ctx.max_dof_blur_uniform, {{max_dof_blur, 0, 0, 0}})
			setUniform(pipeline, ctx.dof_clear_range_uniform, {{dof_clear_range, 0, 0, 0}})
			setUniform(pipeline, ctx.dof_near_multiplier_uniform, {{dof_near_multiplier, 0, 0, 0}})
	else
		newView(pipeline, "ppbasic", "postprocess_result")
			setPass(pipeline, "MAIN")
			disableBlending(pipeline)
			applyCamera(pipeline, camera_slot)
			disableDepthWrite(pipeline)
			clear(pipeline, CLEAR_COLOR | CLEAR_DEPTH, 0x00000000)
			bindFramebufferTexture(pipeline, "linear", 0, ctx.hdr_buffer_uniform)

	end
	if vignette_enabled then 
		setUniform(pipeline, ctx.vignette_uniform, {{vignette_radius, vignette_softness, 0, 0}})
	end
	if film_grain_enabled then
		setUniform(pipeline, ctx.grain_amount_uniform, {{grain_amount, 0, 0, 0}})
		setUniform(pipeline, ctx.grain_size_uniform, {{grain_size, 0, 0, 0}})
	end
	drawQuad(pipeline, 0, 0, 1, 1, ctx.ppbasic_material)

	if fxaa_enabled then
		fxaa(pipeline, ctx, camera_slot)
	else
		newView(pipeline, "ppbasic_final_copy", "linear")
			copyRenderbuffer(pipeline, "postprocess_result", 0, "linear", 0)
	end
end


function initBloom(pipeline, env)
	env.extract_material = Engine.loadResource(g_engine, "pipelines/postprocess_basic/bloomextract.mat", "material")
	env.loom_material = Engine.loadResource(g_engine, "pipelines/postprocess_basic/bloom.mat", "material")
	addFramebuffer(pipeline, "bloom_extract", {
		size_ratio = { 1, 1},
		renderbuffers = {
			{ format = "rgba16f" }
		}
	})

	addFramebuffer(pipeline, "bloom_blur2", {
		size_ratio = { 0.5, 0.5 },
		renderbuffers = {
			{ format = "rgba16f" }
		}
	})
	addFramebuffer(pipeline, "bloom_blur4", {
		size_ratio = { 0.25, 0.25 },
		renderbuffers = {
			{ format = "rgba16f" }
		}
	})
	addFramebuffer(pipeline, "bloom_blur8", {
		size_ratio = { 0.125, 0.125 },
		renderbuffers = {
			{ format = "rgba16f" }
		}
	})
end


function bloom(ctx, pipeline)
	if not bloom_enabled then return end
	
	newView(pipeline, "bloom_extract", "bloom_extract")
		setPass(pipeline, "MAIN")
		disableBlending(pipeline)
		disableDepthWrite(pipeline)
		setUniform(pipeline, ctx.bloom_cutoff, {{bloom_cutoff, 0, 0, 0}})
		bindFramebufferTexture(pipeline, "hdr", 0, ctx.texture_uniform)
		drawQuad(pipeline, 0, 0, 1, 1, ctx.extract_material)
	
	if bloom_blur then
		newView(pipeline, "blur_bloom2_downsample", "bloom_blur2")
			setPass(pipeline, "MAIN")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_extract", 0, ctx.shadowmap_uniform)
			drawQuad(pipeline, 0, 0, 1, 1, ctx.downsample_material)
			enableDepthWrite(pipeline)

		newView(pipeline, "blur_bloom2_h", "bloom_extract")
			setPass(pipeline, "BLUR_H")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_blur2", 0, ctx.shadowmap_uniform)
			drawQuad(pipeline, 0, 0, 0.5, 0.5, ctx.blur_material)
			enableDepthWrite(pipeline)

		newView(pipeline, "blur_bloom2_v", "hdr")
			setPass(pipeline, "BLUR_V")
			enableBlending(pipeline, "add")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_extract", 0, ctx.shadowmap_uniform)
			drawQuadEx(pipeline, 0, 0, 1, 1, 0, 0.5, 0.5, 0, ctx.blur_material);
			enableDepthWrite(pipeline)
			
		newView(pipeline, "blur_bloom4_downsample", "bloom_blur4")
			setPass(pipeline, "MAIN")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_blur2", 0, ctx.shadowmap_uniform)
			drawQuad(pipeline, 0, 0, 1, 1, ctx.downsample_material)
			enableDepthWrite(pipeline)
			
		newView(pipeline, "blur_bloom4_h", "bloom_extract")
			setPass(pipeline, "BLUR_H")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_blur4", 0, ctx.shadowmap_uniform)
			drawQuad(pipeline, 0, 0, 0.25, 0.25, ctx.blur_material)
			enableDepthWrite(pipeline)

		newView(pipeline, "blur_bloom4_v", "hdr")
			setPass(pipeline, "BLUR_V")
			enableBlending(pipeline, "add")
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_extract", 0, ctx.shadowmap_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
			drawQuadEx(pipeline, 0, 0, 1, 1, 0, 0, 0.25, 0.25, ctx.blur_material);
			enableDepthWrite(pipeline)

	end
			
	renderBloomDebug(ctx, pipeline)
end

function fxaa(pipeline, ctx, camera_slot)
	if not fxaa_enabled then return end
	
	newView(pipeline, "fxaa", "linear")
		setPass(pipeline, "MAIN")
		disableBlending(pipeline)
		applyCamera(pipeline, camera_slot)
		disableDepthWrite(pipeline)
		clear(pipeline, CLEAR_DEPTH, 0x00000000)
		bindFramebufferTexture(pipeline, "postprocess_result", 0, ctx.fxaa_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
		drawQuad(pipeline, 0, 0, 1, 1, ctx.fxaa_material)
end

function onDestroy()
	if pipeline_env then
		removeFramebuffer(pipeline_env.pipeline, "dof")
		removeFramebuffer(pipeline_env.pipeline, "dof_blur")
		removeFramebuffer(pipeline_env.pipeline, "bloom_extract")
		removeFramebuffer(pipeline_env.pipeline, "bloom_blur2")
		removeFramebuffer(pipeline_env.pipeline, "bloom_blur4")
		removeFramebuffer(pipeline_env.pipeline, "bloom_blur8")
	end
end

function initPostprocess(pipeline, env)
	pipeline_env = env
	initBloom(pipeline, env)
	initPostprocessBasic(pipeline, env)
end

function renderBloomDebug(ctx, pipeline)
	if bloom_debug then
		newView(pipeline, "bloom_debug", "hdr")
			setPass(pipeline, "MAIN")
			disableBlending(pipeline)
			disableDepthWrite(pipeline)
			bindFramebufferTexture(pipeline, "bloom_extract", 0, ctx.texture_uniform)
			if bloom_debug_fullscreen then
				drawQuad(pipeline, 0, 0, 1, 1, ctx.screen_space_material)
			else
				drawQuad(pipeline, 0.48, 0.48, 0.5, 0.5, ctx.screen_space_material)
			end
	end
end

function postprocess(pipeline, env)
	if enabled then
		camera_cmp = Renderer.getCameraComponent(g_scene_renderer, this)
		slot = Renderer.getCameraSlot(g_scene_renderer, camera_cmp)
		postprocessBasic(pipeline, env, slot)
	end
end


function onGUI()
	local changed
	changed, bloom_debug = ImGui.Checkbox("Bloom debug", bloom_debug)
	if bloom_debug then
		changed, bloom_blur = ImGui.Checkbox("Bloom blur", bloom_blur)
		ImGui.SameLine()
		changed, bloom_debug_fullscreen = ImGui.Checkbox("Fullscreen", bloom_debug_fullscreen)
	end
end
