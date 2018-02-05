collider = -1
Editor.setPropertyType("collider", Editor.ENTITY_PROPERTY)

local yaw = 0
local actions = {
	forward = false,
	left = false,
	right = false,
	back = false
}

function onInputEvent(event)
	if event.type == Engine.INPUT_EVENT_BUTTON then
		if event.device.type == Engine.INPUT_DEVICE_KEYBOARD then
			if event.scancode == Engine.INPUT_SCANCODE_W then
				actions.forward = event.state ~= 0
			elseif event.scancode == Engine.INPUT_SCANCODE_A then
				actions.left = event.state ~= 0
			elseif event.scancode == Engine.INPUT_SCANCODE_D then
				actions.right = event.state ~= 0
			elseif event.scancode == Engine.INPUT_SCANCODE_S then
				actions.back = event.state ~= 0
			end
		end		
	end
end

function minusVec3(v)
	return {-v[1], -v[2], -v[3]}
end

function mulVec3(v, f)
	return {v[1] * f, v[2] * f, v[3] * f}
end

function magnitude(v)
	return math.sqrt(v[1] * v[1] + v[2] * v[2] + v[3] * v[3])
end

function normalized(v)
	return mulVec3(v, 1/magnitude(v))
end

function getRoll(dir, velocity)
	local mag = magnitude(velocity)
	local d = normalized({dir[3], 0, -dir[1]})
	local v = normalized(velocity)
	local t = d[1] * v[1] + d[2] * v[2] + d[3] * v[3]
	return -(math.pi * 0.5 - math.acos(t)) * mag / 30
end

function makeQuat(axis, angle)
	local half_angle = angle * 0.5
	local s = math.sin(half_angle)
	return {
		axis[1] * s,
		axis[2] * s,
		axis[3] * s,
		math.cos(half_angle)
	}
end

function mulQuatQuat(a, b)
	return {
		a[4] * b[1] + b[4] * a[1] + a[2] * b[3] - b[2] * a[3],
		a[4] * b[2] + b[4] * a[2] + a[3] * b[1] - b[3] * a[1],
		a[4] * b[3] + b[4] * a[3] + a[1] * b[2] - b[1] * a[2],
		a[4] * b[4] - a[1] * b[1] - a[2] * b[2] - a[3] * b[3]
	}
end

function getYawRollQuat(yaw, roll)
	local y = makeQuat({0, 1, 0}, yaw)
	local r = makeQuat({0, 0, 1}, roll)
	
	return mulQuatQuat(y, r)
end

function easeInOutQuad(t, b, c, d) 
	t = t / d/2
	if (t < 1) then return c/2*t*t + b end
	t = t - 1
	return -c/2 * (t*(t-2) - 1) + b
end

function getDriveForceMagnitude(velocity)
	local mag = magnitude(velocity)
	local t = mag / 100
	if t > 1 then t = 1 end
	return easeInOutQuad(t, 50, 100, 1)
end

function update(time_delta)
	local dir = {math.sin(yaw), 0, math.cos(yaw)}
	local velocity = Physics.getActorVelocity(g_scene_physics, collider)
	local vel_mag = magnitude(velocity)
	local inv_vel = minusVec3(velocity)
	local drag = mulVec3(inv_vel, 8.0)
	Physics.applyForceToActor(g_scene_physics, collider, drag)
	
	local pos = Engine.getEntityPosition(g_universe, collider)
	pos[2] = pos[2] - 0.7
	Engine.setEntityPosition(g_universe, this, pos)
	local roll = getRoll(dir, velocity)
	local yaw_roll = getYawRollQuat(yaw, roll)
	Engine.setEntityRotation(g_universe, this, yaw_roll)
	
	if vel_mag > 0.1 then
		Physics.applyForceToActor(g_scene_physics, collider, mulVec3(dir, vel_mag * 4))
	end
	
	if actions.forward then
		local force_magnitude = getDriveForceMagnitude(velocity)
		Physics.applyForceToActor(g_scene_physics, collider, mulVec3(dir, force_magnitude))
	end
	if actions.back then
		Physics.applyForceToActor(g_scene_physics, collider, drag)
	end
	if actions.left then
		yaw = yaw + time_delta * vel_mag / 3
	end
	if actions.right then
		yaw = yaw - time_delta * vel_mag / 3
	end
end