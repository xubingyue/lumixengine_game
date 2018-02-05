function importDir(src_dir, out_dir, center_bottom)
	for filename in io.popen([[dir "]] .. src_dir .. [[" /b]]):lines() do 
		if string.sub(filename,-string.len(".FBX")):upper() ==".FBX" and string.find(filename, "_LOD") == nil then
			importModel(src_dir .. filename, out_dir, center_bottom)
		end
	end
end

function importModel(filename, out_dir, center_bottom)
	Engine.logInfo("Importing model " .. filename .. "...")
	ImportAsset.clearSources()
	ImportAsset.addSource(filename)
	
	if center_bottom then
		ImportAsset.setParams({
			output_dir = out_dir,
			scale = 1.0,
			origin = "bottom"
		})
	else
		ImportAsset.setParams({
			output_dir = out_dir,
			scale = 1.0,
		})
	end
	
	for i = 0, ImportAsset.getMeshesCount() - 1 do	
		ImportAsset.setMeshParams(i, {import_physics = true})
	end
	
	for i = 0, ImportAsset.getMaterialsCount() - 1 do
		local args = {import = true, alpha_cutout = false}
		local name = ImportAsset.getMaterialName(i)
		ImportAsset.setMaterialParams(i, args)
		
	end

	
	ImportAsset.import()
	Engine.logInfo(filename .. " imported")
end

--importDir([[source_assets/naturepack/]], [[models/environment/]], false)
--importDir([[source_assets/weaponpack/]], [[models/weapons/]], false)
importDir([[source_assets/track/]], [[models/track/]], true)