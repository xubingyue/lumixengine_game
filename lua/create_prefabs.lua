function createPrefab(filename)
	local phy_filename = string.sub(filename, 1, -string.len(".msh")) .. "phy"
	local fab_filename = string.sub(filename, 1, -string.len(".msh")) .. "fab"
	local entity = Editor.createEntityEx(Editor.editor, { 
		renderable = {Source = [[models/environment/]]..filename}, 
		mesh_rigid_actor = {Source = [[models/environment/]]..phy_filename} 
	})
	
	--Editor.selectEntity(Editor.editor, entity)
	--Editor.savePrefab(Editor.editor, [[prefabs/]]..fab_filename)
end

local i = 0
for filename in io.popen([[dir "models\environment\" /b]]):lines() do 
	if string.sub(filename,-string.len(".MSH")):upper() ==".MSH" then
		createPrefab(filename)
		i = i + 1
		if i > 50 then return end
	end
end

