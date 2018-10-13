<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class User extends Model
{
    protected $fillable = [
        'username', 'email', 'password', 'role',
    ];

    protected $hidden = [
        'password',
    ];

    public function coin() {
        return $this->hasMany('App\Coin','id_user','id');
    }

    public function cash() {
        return $this->hasMany('App\Cash','id_user','id');
    }
}
