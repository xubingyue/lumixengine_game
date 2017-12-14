common = require "pipelines/common"
ctx = { pipeline = this }
if APP then
	camera = "main"
elseif GAME_VIEW  then
	camera = "main"
else
	camera = "editor"
end

local DEFAULT_RENDER_MASK = 1
local TRANSPARENT_RENDER_MASK = 2
local WATER_RENDER_MASK = 4
local FUR_RENDER_MASK = 8
local ALL_RENDER_MASK = DEFAULT_RENDER_MASK + TRANSPARENT_RENDER_MASK + WATER_RENDER_MASK + FUR_RENDER_MASK
local screenshot_request = 0
local render_fur = true
local render_shadowmap = true
local disable_render = false
local render_debug_deferred = 
{ 
 { label = "Albedo", enabled = false, fullscreen = false, mask = {1, 1, 1, 0}, g_buffer_idx = 0},
 { label = "Normal", enabled = false, fullscreen = false, mask = {1, 1, 1, 0}, g_buffer_idx = 1},
 { label = "Roughness", enabled = false, fullscreen = false, mask = {0, 0, 0, 1}, g_buffer_idx = 0},
 { label = "Metallic", enabled = false, fullscreen = false, mask = {0, 0, 0, 1}, g_buffer_idx = 1},
 { label = "AO", enabled = false, fullscreen = false, mask = {1, 1, 1, 0}, g_buffer_idx = 2},
 { label = "Depth", enabled = false, fullscreen = false, mask = {1, 0, 0, 0}, g_buffer_idx = 3},
}

addFramebuffer(this, "linear", {
	width = 1024,
	height = 1024,
	size_ratio = {1, 1},
	renderbuffers = {
		{ format = "rgba8" },
		{ format = "depth24stencil8" }
	}
})

addFramebuffer(ctx.pipeline, "lum256", {
	width = 256,
	height = 256,
	renderbuffers = {
		{ format = "r32f" }
	}
})

addFramebuffer(ctx.pipeline,  "lum64", {
	width = 64,
	height = 64,
	renderbuffers = {
		{ format = "r32f" }
	}
})

addFramebuffer(ctx.pipeline,  "lum16", {
	width = 16,
	height = 16,
	renderbuffers = {
		{ format = "r32f" }
	}
})

addFramebuffer(ctx.pipeline,  "lum4", {
	width = 4,
	height = 4,
	renderbuffers = {
		{ format = "r32f" }
	}
})

addFramebuffer(ctx.pipeline,  "lum1a", {
	width = 1,
	height = 1,
	renderbuffers = {
		{ format = "r32f" }
	}
})

addFramebuffer(ctx.pipeline,  "lum1b", {
	width = 1,
	height = 1,
	renderbuffers = {
		{ format = "r32f" }
	}
})

addFramebuffer(ctx.pipeline,  "hdr", {
	width = 1024,
	height = 1024,
	screen_size = true,
	renderbuffers = {
		{ format = "rgba16f" },
		{ format = "depth24stencil8" }
	}
})

if not APP then
	addFramebuffer(this, "default", {
		width = 1024,
		height = 1024,
		renderbuffers = {
			{ format = "rgba8" },
			{ format = "depth24stencil8" }
		}
	})
end
	
if SCENE_VIEW then
	addFramebuffer(this, "selection_mask", {
		width = 1024,
		height = 1024,
		size_ratio = {1, 1},
		renderbuffers = {
			{ format = "rgba8" }
		}
	})
end
	
addFramebuffer(this, "g_buffer", {
	width = 1024,
	height = 1024,
	screen_size = true,
	renderbuffers = {
		{ format = "rgba8" },
		{ format = "rgba16f" },
		{ format = "rgba8" },
		{ format = "depth24stencil8" }
	}
})
  
common.init(ctx)
common.initShadowmap(ctx, 1024)

local texture_uniform = createUniform(this, "u_texture")
local selection_outline_material = Engine.loadResource(g_engine, "pipelines/common/selection_outline.mat", "material")
local screen_space_material = Engine.loadResource(g_engine, "pipelines/screenspace/screenspace.mat", "material")
local gbuffer0_uniform = createUniform(this, "u_gbuffer0")
local gbuffer1_uniform = createUniform(this, "u_gbuffer1")
local gbuffer2_uniform = createUniform(this, "u_gbuffer2")
local gbuffer_depth_uniform = createUniform(this, "u_gbuffer_depth")
local irradiance_map_uniform = createUniform(this, "u_irradiance_map")
local radiance_map_uniform = createUniform(this, "u_radiance_map")
local lum_size_uniform = createVec4ArrayUniform(this, "u_offset", 16)
local hdr_buffer_uniform = createUniform(this, "u_hdrBuffer")
local avg_luminance_uniform = createUniform(ctx.pipeline, "u_avgLuminance")
local pbr_material = Engine.loadResource(g_engine, "pipelines/pbr/pbr.mat", "material")
local pbr_local_light_material = Engine.loadResource(g_engine, "pipelines/pbr/pbrlocallight.mat", "material")
local gamma_mapping_material = Engine.loadResource(g_engine, "pipelines/common/gamma_mapping.mat", "material")
local tonemap_material = Engine.loadResource(g_engine, "pipelines/common/tonemap.mat", "material")
local lum_material = Engine.loadResource(g_engine, "pipelines/common/extractluminance.mat", "material")

function deferred(camera_slot)
	deferred_view = newView(this, "geometry_pass", "g_buffer", DEFAULT_RENDER_MASK)
		setPass(this, "DEFERRED")
		applyCamera(this, camera_slot)
		clear(this, CLEAR_ALL, 0x00000000)
		
		setStencil(this, STENCIL_OP_PASS_Z_REPLACE 
			| STENCIL_OP_FAIL_Z_KEEP 
			| STENCIL_OP_FAIL_S_KEEP 
			| STENCIL_TEST_ALWAYS)
		setStencilRMask(this, 0xff)
		setStencilRef(this, 1)
	
	newView(this, "clear_main", "hdr")
		-- there are strange artifacts on some platforms without this clear
		clear(this, CLEAR_ALL, 0x00000000)
	
	newView(this, "copyRenderbuffer", "hdr");
		copyRenderbuffer(this, "g_buffer", 3, "hdr", 1)

	newView(this, "decals", "g_buffer")
		setPass(this, "DEFERRED")
		disableDepthWrite(this)
		applyCamera(this, camera_slot)
		bindFramebufferTexture(this, "hdr", 1, gbuffer_depth_uniform)
		renderDecalsVolumes(this)
		
	newView(this, "light_pass", "hdr")
		setPass(this, "MAIN")
		applyCamera(this, camera_slot)
		clear(this, CLEAR_COLOR, 0x00000000)
		
		disableDepthWrite(this)
		setActiveGlobalLightUniforms(this)
		bindFramebufferTexture(this, "g_buffer", 0, gbuffer0_uniform)
		bindFramebufferTexture(this, "g_buffer", 1, gbuffer1_uniform)
		bindFramebufferTexture(this, "g_buffer", 2, gbuffer2_uniform)
		bindFramebufferTexture(this, "g_buffer", 3, gbuffer_depth_uniform)
		bindEnvironmentMaps(this, irradiance_map_uniform, radiance_map_uniform)
		drawQuad(this, 0, 0, 1, 1, pbr_material)
		
	newView(this, "local_light_pass", "hdr")
		setPass(this, "MAIN")
		disableDepthWrite(this)
		enableBlending(this, "add")
		applyCamera(this, camera_slot)
		bindFramebufferTexture(this, "g_buffer", 0, gbuffer0_uniform)
		bindFramebufferTexture(this, "g_buffer", 1, gbuffer1_uniform)
		bindFramebufferTexture(this, "g_buffer", 2, gbuffer2_uniform)
		bindFramebufferTexture(this, "g_buffer", 3, gbuffer_depth_uniform)
		renderLightVolumes(this, pbr_local_light_material)
		disableBlending(this)

	newView(this, "deferred_debug_shapes", "hdr")
		setPass(this, "EDITOR")
		applyCamera(this, camera_slot)
		setStencil(this, STENCIL_OP_PASS_Z_REPLACE 
			| STENCIL_OP_FAIL_Z_KEEP 
			| STENCIL_OP_FAIL_S_KEEP 
			| STENCIL_TEST_ALWAYS)
		setStencilRMask(this, 0xff)
		setStencilRef(this, 1)
		renderDebugShapes(this)	
	
end


function water()
	water_view = newView(this, "WATER", "hdr", WATER_RENDER_MASK)
		setPass(this, "MAIN")
		disableDepthWrite(this)
		applyCamera(this, camera)
		setActiveGlobalLightUniforms(this)
		bindFramebufferTexture(this, "g_buffer", 0, gbuffer0_uniform) -- refraction
		bindFramebufferTexture(this, "g_buffer", 1, gbuffer1_uniform) 
		bindFramebufferTexture(this, "g_buffer", 2, gbuffer2_uniform) 
		bindFramebufferTexture(this, "g_buffer", 3, gbuffer_depth_uniform) -- depth
		bindEnvironmentMaps(this, irradiance_map_uniform, radiance_map_uniform)
end

function renderSelectionOutline(ctx)
	newView(this, "selection_mask", "selection_mask", ALL_RENDER_MASK)
		clear(this, CLEAR_COLOR, 0)
		setPass(this, "SHADOW")
		disableDepthWrite(this)
		applyCamera(this, camera)
		renderSelection(this)

	newView(this, "selection_outline", "default")
		setPass(this, "MAIN")
		disableDepthWrite(this)
		bindFramebufferTexture(this, "selection_mask", 0, texture_uniform)
		drawQuad(this, 0, 0, 1, 1, selection_outline_material)

end

function transparency()
	newView(this, "TRANSPARENT", "hdr", TRANSPARENT_RENDER_MASK)
		setViewMode(this, VIEW_MODE_DEPTH_DESCENDING)
		setPass(this, "FORWARD")
		disableDepthWrite(this)
		enableBlending(this, "alpha")
		applyCamera(this, camera)
		setActiveGlobalLightUniforms(this)
		bindEnvironmentMaps(this, irradiance_map_uniform, radiance_map_uniform)
end

function fur()
	if not render_fur then return end
	fur_view = newView(this, "FUR", "hdr", FUR_RENDER_MASK)
		setPass(this, "FUR")
		disableDepthWrite(this)
		enableBlending(this, "alpha")
		applyCamera(this, camera)
		setActiveGlobalLightUniforms(this)
		bindEnvironmentMaps(this, irradiance_map_uniform, radiance_map_uniform)
end


function renderDebug(ctx)
	local offset_x = 0
	local offset_y = 0
	for i, _ in ipairs(render_debug_deferred) do
		if render_debug_deferred[i].enabled then
			newView(this, "deferred_debug_"..tostring(i), "default")
				setPass(this, "MAIN")
				bindFramebufferTexture(this, "g_buffer", render_debug_deferred[i].g_buffer_idx, ctx.texture_uniform)
				setUniform(this, ctx.multiplier_uniform, {render_debug_deferred[i].mask})
				drawQuad(this, 0.01 + offset_x, 0.01 + offset_y, 0.23, 0.23, ctx.screen_space_debug_material)
				
			offset_x = offset_x + 0.25
			if offset_x > 0.76 then
				offset_x = 0.0
				offset_y = offset_y + 0.25
			end
		end
	end
	common.shadowmapDebug(ctx, offset_x, offset_y)
	for i, _ in ipairs(render_debug_deferred) do
		if render_debug_deferred[i].enabled and render_debug_deferred[i].fullscreen then
			newView(this, "deferred_debug_fullsize", "default")
				setPass(this, "MAIN")
				bindFramebufferTexture(this, "g_buffer", render_debug_deferred[i].g_buffer_idx, ctx.texture_uniform)
				setUniform(this, ctx.multiplier_uniform, {render_debug_deferred[i].mask})
				drawQuad(this, 0, 0, 1, 1, ctx.screen_space_debug_material)
		end
	end
end

function ingameGUI()
	newView(this, "ingame_gui", "default")
		setPass(this, "MAIN")
		clear(this, CLEAR_DEPTH, 0x303030ff)
		renderIngameGUI(this)
end

local current_lum1 = "lum1a"
local lum_uniforms = {}

function computeLumUniforms()
	local sizes = {256, 64, 16, 4, 1 }
	for key,value in ipairs(sizes) do
		lum_uniforms[value] = {}
		for j = 0,1 do
			for i = 0,1 do
				local x = 1 / (4 * value) + i / (2 * value) - 1 / (2 * value)
				local y = 1 / (4 * value) + j / (2 * value) - 1 / (2 * value)
				lum_uniforms[value][1 + i + j * 2] = {x, y, 0, 0}
			end
		end
	end
end
computeLumUniforms()

function extractLuminance()
	newView(this, "hdr_luminance", "lum256")
		setPass(this, "HDR_EXTRACT_LUMINANCE")
		disableDepthWrite(this)
		disableBlending(this)
		setUniform(this, lum_size_uniform, lum_uniforms[256])
		bindFramebufferTexture(this, "hdr", 0, hdr_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
		drawQuad(this, 0, 0, 1, 1, lum_material)
	
	newView(this, "lum64", "lum64")
		setPass(this, "MAIN")
		setUniform(this, lum_size_uniform, lum_uniforms[64])
		bindFramebufferTexture(this, "lum256", 0, hdr_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
		drawQuad(this, 0, 0, 1, 1, lum_material)

	newView(this, "lum16", "lum16")
		setPass(this, "MAIN")
		setUniform(this, lum_size_uniform, lum_uniforms[16])
		bindFramebufferTexture(this, "lum64", 0, hdr_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
		drawQuad(this, 0, 0, 1, 1, lum_material)
	
	newView(this, "lum4", "lum4")
		setPass(this, "MAIN")
		setUniform(this, lum_size_uniform, lum_uniforms[4])
		bindFramebufferTexture(this, "lum16", 0, hdr_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
		drawQuad(this, 0, 0, 1, 1, lum_material)

	local old_lum1 = "lum1b"
	if current_lum1 == "lum1a" then 
		current_lum1 = "lum1b" 
		old_lum1 = "lum1a"
	else 
		current_lum1 = "lum1a" 
	end

	newView(this, "lum1", current_lum1)
		setPass(this, "FINAL")
		setUniform(this, lum_size_uniform, lum_uniforms[1])
		bindFramebufferTexture(this, "lum4", 0, hdr_buffer_uniform, TEXTURE_MAG_ANISOTROPIC | TEXTURE_MIN_ANISOTROPIC)
		bindFramebufferTexture(this, old_lum1, 0, avg_luminance_uniform)
		drawQuad(this, 0, 0, 1, 1, lum_material)
end

function tonemapping()
	extractLuminance()
	
	newView(this, "tonemap", "linear")
		setMaterialDefine(this, tonemap_material, "FIXED_EXPOSURE", SCENE_VIEW ~= nil)
		setPass(this, "MAIN")
		clear(this, CLEAR_DEPTH, 0x303030ff)
		bindFramebufferTexture(this, "hdr", 0, texture_uniform)
		bindFramebufferTexture(this, current_lum1, 0, avg_luminance_uniform)
		drawQuad(this, 0, 0, 1, 1, tonemap_material)
end

function render()
	if disable_render then
		newView(this, "render_disable", "default")
			clear(this, CLEAR_ALL, 0x00000000)
			setPass(this, "MAIN")
		return
	end

	if render_shadowmap then
		common.shadowmap(ctx, camera, DEFAULT_RENDER_MASK + FUR_RENDER_MASK)
	end
	deferred(camera)
	common.particles(ctx, camera)

	doPostprocess(this, _ENV, "pre_transparent", camera)

	water()
	fur()
	transparency()
	
	renderModels(this, ALL_RENDER_MASK)
	
	doPostprocess(this, _ENV, "main", camera)
	
	tonemapping()
	
	doPostprocess(this, _ENV, "post_tonemapping", camera)
	
	newView(this, "draw2d", "hdr")
		setPass(this, "MAIN")
		render2D(this)
	
	newView(this, "SRGB", "default")
		clear(this, CLEAR_ALL, 0x00000000)
		setPass(this, "MAIN")
		bindFramebufferTexture(this, "linear", 0, texture_uniform)
		drawQuad(this, 0, 0, 1, 1, gamma_mapping_material)

	if SCENE_VIEW then
		common.renderEditorIcons(ctx)
		common.renderGizmo(ctx)
		renderDebug(ctx)
		renderSelectionOutline(ctx)
	end
	if GAME_VIEW or APP then
		ingameGUI()
	end
	
	if screenshot_request > 1 then
		-- we have to wait for a few frames to propagate changed resolution to ingame gui
		-- only then we can take a screeshot
		-- otherwise ingame gui would be constructed in gameview size
		-- 1st frame - set forceViewport
		-- 2nd frame - set ImGui's display size (ingame) to forced value
		-- 3rd frame - construct ingame gui with forced values
		-- 4th frame - render and save (save is internally two more frames)
		screenshot_request = screenshot_request - 1
		GameView.forceViewport(true, 4096, 2160)
	elseif screenshot_request == 1 then
		saveRenderbuffer(this, "default", 0, "screenshot.tga")
		GameView.forceViewport(false, 0, 0)
		screenshot_request = 0
	end
end

local volume = 1
local paused = false
local timescale = 1

function onGUI()
	if GAME_VIEW then
		ImGui.SameLine()
		if ImGui.Button("Screenshot") then
			screenshot_request = 4
		end
		return
	end

	local changed
	ImGui.SameLine()
	changed, volume = ImGui.SliderFloat("Volume", volume, 0, 1)
	if changed then
		Audio.setMasterVolume(g_scene_audio, volume)
	end
	ImGui.SameLine()
	if ImGui.Button("Debug") then
		ImGui.OpenPopup("debug_popup")
	end

	ImGui.SameLine()
	changed, paused = ImGui.Checkbox("paused", paused)
	if changed then
		Engine.pause(g_engine, paused)
	end
	
	ImGui.SameLine()
	if ImGui.Button("Next frame") then Engine.nextFrame(g_engine) end
	
	ImGui.SameLine()
	changed, timescale = ImGui.SliderFloat("Timescale", timescale, 0, 1)
	if changed then
		Engine.setTimeMultiplier(g_engine, timescale)
	end
	
	
	if ImGui.BeginPopup("debug_popup") then
		for i, _ in ipairs(render_debug_deferred) do
			changed, render_debug_deferred[i].enabled = ImGui.Checkbox(render_debug_deferred[i].label, render_debug_deferred[i].enabled)
			if render_debug_deferred[i].enabled then
				ImGui.SameLine()
				changed, render_debug_deferred[i].fullscreen = ImGui.Checkbox("Fullsize###gbf" .. tostring(i), render_debug_deferred[i].fullscreen)
				
				if changed and render_debug_deferred[i].fullscreen then
					for j, _ in ipairs(render_debug_deferred) do
						render_debug_deferred[j].fullscreen = false
					end
					render_debug_deferred[i].fullscreen = true
				end
			end
		end
		
		changed, common.render_shadowmap_debug = ImGui.Checkbox("Shadowmap", common.render_shadowmap_debug)
		if common.render_shadowmap_debug then
			ImGui.SameLine()
			changed, common.render_shadowmap_debug_fullsize = ImGui.Checkbox("Fullsize###gbfsm", common.render_shadowmap_debug_fullsize)
		end
		if ImGui.Button("High details") then
			Renderer.setGlobalLODMultiplier(g_scene_renderer, 0.1)
		end
		if ImGui.Button("Toggle") then
			local v = not render_debug_deferred[1].enabled
			common.render_shadowmap_debug = v 
			render_debug_deferred[1].enabled = v
			render_debug_deferred[2].enabled = v
			render_debug_deferred[3].enabled = v
			render_debug_deferred[4].enabled = v
		end
		changed, disable_render = ImGui.Checkbox("Disabled rendering", disable_render)
		changed, render_shadowmap = ImGui.Checkbox("Render shadowmap", render_shadowmap)
		changed, common.blur_shadowmap = ImGui.Checkbox("Blur shadowmap", common.blur_shadowmap)
		changed, common.render_gizmos = ImGui.Checkbox("Render gizmos", common.render_gizmos)
		changed, render_fur = ImGui.Checkbox("Render fur", render_fur)
		
		ImGui.EndPopup()
	end
end