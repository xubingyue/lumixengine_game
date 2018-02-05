function createPrefab(filename)
	local phy_filename = string.sub(filename, 1, -string.len(".msh")) .. "phy"
	local fab_filename = string.sub(filename, 1, -string.len(".msh")) .. "fab"
	local entity = Editor.createEntityEx(Editor.editor, { 
		renderable = {Source = [[models/environment/]]..filename}, 
		mesh_rigid_actor = {Source = [[models/environment/]]..phy_filename} 
	})
	
	Editor.selectEntity(entity)
	Editor.savePrefabAs([[prefabs/]]..fab_filename)
	Editor.destroyEntity(entity)
end

for filename in io.popen([[dir "models\environment\" /b]]):lines() do 
	if string.sub(filename,-string.len(".MSH")):upper() ==".MSH" then
		createPrefab(filename)
	end
end

