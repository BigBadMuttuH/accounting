-- DROP VIEW accounting_view; 
CREATE VIEW accounting_view AS
SELECT
	a.id,
	a.user_id,
	a.device_id,
	a.connection_permission_id,
	a.disconnection_permission_id,
	cp.permission_date as RegDate,
	d.inventory_number as InvetoryNumber,
	d.model as device_model,
	d.serial_number as SerialNumber,
	u.display_name as FIO,
	u.department as Department,
	cp.permission_number as ConnectionPermissionNumber,
	dcp.permission_number as DisconnectionPermissionNumber
FROM accounting a
JOIN ad_user u ON a.user_id = u.sid
JOIN device d ON a.device_id = d.id
JOIN connection_permission cp ON a.connection_permission_id = cp.id
LEFT JOIN connection_permission dcp on a.disconnection_permission_id = dcp.id;