<?php
//Server login Variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
//User variables
	$username = $_POST["username"];
	$password = $_POST["password"];

//Check connection
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if (!$conn)
	{
		die("Connection failed. ".mysqli_connect_error());
	}
	
//Check user exists
	$namecheckquery = "SELECT username, salt, hash FROM users WHERE username = '".$username."';";
	$namecheck = mysqli_query($conn, $namecheckquery);
	if (mysqli_num_rows($namecheck) != 1)
	{
		echo "User is Incorrect";
		exit();
	}
	
//Get login from query
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["hash"];
	
	$loginhash = crypt($password, $salt);
	if ($hash != $loginhash)
	{
		echo "Incorrect Pasword";
		exit();
	}
	else
	{
		echo "Logged In";
		exit();
	}
?>