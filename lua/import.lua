function importDir(src_dir)
	for filename in io.popen([[dir "]] .. src_dir .. [[" /b]]):lines() do 
		if string.sub(filename,-string.len(".FBX")):upper() ==".FBX" and string.find(filename, "_LOD") == nil then
			importModel(src_dir .. filename, [[models/environment/]])
		end
	end
end

function importModel(filename, out_dir)
	Engine.logInfo("Importing model " .. filename .. "...")
	ImportAsset.clearSources()
	ImportAsset.addSource(filename)
	ImportAsset.setParams({
		output_dir = out_dir,
		scale = 1.0,
	})
	
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

importDir([[source_assets/naturepack/]])