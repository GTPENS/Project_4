<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\User;
use App\Coin;

class AdminController extends Controller
{
    public function index() {
        $data = User::paginate(10);
        return view('admin',compact('data'));
    }

    public function delete($id) {
        $data = User::where('id',$id)->first();
        $data->delete();
        return redirect()->back();
    }

    public function detail($id) {
        $data = User::where('id',$id)->first();
        $coin = Coin::where('id_user',$id)->sum('coin');
        echo 'username ' . $data->username . ' : ' . $coin;
    }
}
