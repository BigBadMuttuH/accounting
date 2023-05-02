CREATE VIEW accounting_view AS
SELECT
	a.id as ID,
	cp.permission_date as RegDate,
	d.inventory_number as InvetoryNumber,
	d.serial_number as SerialNumber,
	u.display_name as FIO,
	u.department as Department,
	cp.permission_number as PermissionNumber,
	cp.url as URL
FROM accounting a
JOIN ad_user u ON a.user_id = u.sid
JOIN device d ON a.device_id = d.id
JOIN connection_permission cp ON a.connection_permission_id = cp.id;