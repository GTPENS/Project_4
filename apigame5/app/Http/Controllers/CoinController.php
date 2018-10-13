<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Coin;
use App\User;
use Illuminate\Support\Facades\Validator;

class CoinController extends Controller
{
    public function getUserCoin($id) {
        $coin = Coin::where('id_user',$id)->sum('coin');
        return response()->json(compact('coin'), 200);
    }

    public function addUserCoin(Request $r) {
        $validator = Validator::make($r->all(), [
            'id_user' => 'required|integer',
            'coin' => 'required|integer',
        ]);

        if($validator->fails()){
            return response()->json($validator->errors()->toJson(), 400);
        }

        $coin = Coin::create([
            'id_user' => $r->id_user,
            'coin' => $r->coin,
        ]);

        $user = User::where('id',$r->id_user)->first();
        $message = "user : " . $user->username . " get " . $coin->coin . " coin";
        return response()->json(compact('message'),201);
    }
}
