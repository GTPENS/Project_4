<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\User;
use App\Coin;
use App\Cash;
use Illuminate\Support\Facades\Validator;

class UserController extends Controller
{
    public function register(Request $r) {
        $validator = Validator::make($r->all(), [
            'username' => 'required|string|max:64|unique:users|',
            'email' => 'required|string|email|max:255|unique:users',
            'password' => 'required|string|min:6',
        ]);

        if($validator->fails()){
            return response()->json($validator->errors()->toJson(), 400);
        }

        $user = User::create([
            'username' => $r->username,
            'email' => $r->email,
            'password' => $r->password,
            'role' => 'User'
        ]);     

        //return response()->json(compact('user'),201);
        return response()->json($user);

    }

    public function login(Request $r) {
        $user = User::where('username',$r->username)->where('password',$r->password)->count();
        if($user == 1) {
            $user = User::where('username',$r->username)->where('password',$r->password)->first();
            //$coin = Coin::where('id_user',$user[0]->id)->sum('coin');
            //$cash = Cash::where('id_user',$user[0]->id)->where('status','Done')->sum('cash');    
            //return response()->json(compact('user','coin','cash'),200);
            return response()->json($user);
        }

        $data = "User Not Found!";
        //return response()->json(compact('data'),401);
        return response()->json($data);
    }
}
