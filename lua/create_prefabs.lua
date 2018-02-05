function createPrefab(filename, dir, create_dest_dir)
	local phy_filename = string.sub(filename, 1, -string.len(".msh")) .. "phy"
	local fab_filename = string.sub(filename, 1, -string.len(".msh")) .. "fab"
	local entity = Editor.createEntityEx(Editor.editor, { 
		renderable = {Source = [[models/]] .. dir .. [[/]]..filename}, 
		mesh_rigid_actor = {Source = [[models/]] .. dir .. [[/]]..phy_filename} 
	})
	
	Editor.selectEntity(entity)
	if create_dest_dir then
		Editor.savePrefabAs([[prefabs/]].. dir .. "/" .. fab_filename)
	else
		Editor.savePrefabAs([[prefabs/]]..fab_filename)
	end
	Editor.destroyEntity(entity)
end

function createPrefabsFromDir(dir, create_dest_dir)
	for filename in io.popen([[dir "models\]] .. dir .. [[\" /b]]):lines() do 
		if string.sub(filename,-string.len(".MSH")):upper() ==".MSH" then
			createPrefab(filename, dir, create_dest_dir)
		end
	end
end

--createPrefabsFromDir("environment", false)
createPrefabsFromDir("track", true)
