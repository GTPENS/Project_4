<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::get('/user', function (Request $request) {
    return $request->user();
})->middleware('auth:api');

Route::get('', function() {
    $data = "Welcome to the beginning of nothingness~";
    return response()->json(compact('data'));
});

Route::post('/user/register', 'UserController@register');
Route::post('/user/login', 'UserController@login');

Route::get('/coin/get/{id}', 'CoinController@getUserCoin');
Route::post('/coin/add', 'CoinController@addUserCoin');