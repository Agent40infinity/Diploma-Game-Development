<?php
//Server login Variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
//User variables
	$username = $_POST["username"];
	$password = $_POST["password"];
	$email = $_POST["email"];

//Check connection
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if (!$conn)
	{
		die("Connection failed. ".mysqli_connect_error());
	}
	
	//Name Check
	$namecheckquery = "SELECT username FROM users WHERE username = '".$username."'";
	$namecheck = mysqli_query($conn,$namecheckquery);
	if (mysqli_num_rows($namecheck)>0)
	{
		echo "Username Already Exists";
		exit();
	}
	
	//Username
	$salt = "\$5\$round=5000\$"."OMEGALUL".$username."\$";
	$hash  = crypt($password, $salt);
	$insertuserquery = "INSERT INTO users (username, email, hash, salt) VALUE ('".$username."', '".$email."', '".$hash."', '".$salt."');";
	mysqli_query($conn, $insertuserquery) or die ("error insert failed");
	echo "Success";
	
	
?>