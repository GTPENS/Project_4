<!DOCTYPE html>
<html lang="en">
<head>
  <title>List User</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>

<div class="container">
  <h2>List User</h2>           
  <table class="table table-bordered">
    <thead>
      <tr>
        <th>Username</th>
        <th>Password</th>
        <th>Email</th>
        <th>Role</th>
        <th>Action</th>
      </tr>
    </thead>
    <tbody>
      @foreach($data as $u)
      <tr>
        <td><a href="{{url('/admin/detail')}}/{{$u->id}}">{{$u->username}}</a></td>
        <td>
            @if($u->role  == 'Admin')
              *****
            @else
              {{$u->password}}
            @endif
        </td>
        <td>{{$u->email}}</td>
        <td>{{$u->role}}</td>
        <td>
            @if($u->role  == 'Admin')
              Not Allowed
            @else
              <form method="POST" action="{{url('/admin/delete')}}/{{$u->id}}">
                {{ csrf_field() }}
                {{ method_field('DELETE') }}
                <button type="submit">Delete</button>
              </form>
            @endif        
        </td>
      </tr>
      @endforeach
    </tbody>
  </table>
  <center>{{$data->links()}}</center>
</div>

</body>
</html>
