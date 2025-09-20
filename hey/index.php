<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Student Registration</title>
  <link rel="stylesheet" href="style.css">
</head>

<body>
  <h2>Student Registration Form</h2>
  <form action="save.php" method="POST">
    <label for="name">Full Name:</label>
    <input type="text" name="name" id="name" required><br><br>

    <label for="id">Student ID:</label>
    <input type="text" name="id" id="id" required><br><br>

    <label for="course">Course:</label>
    <input type="text" name="course" id="course" required><br><br>

    <button type="submit" name="register">Register</button>
    <button type="submit" name="clear">Clear All</button>
  </form>
</body>
</html>
