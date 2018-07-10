var express = require('express');
var router = express.Router();
var promise = require('bluebird');
var options = {promiseLib: promise};
var pgp = require('pg-promise')(options);
var connectionString = 'postgres://postgres@localhost:5432/Torneo';
var db = pgp(connectionString);

module.exports = {
  obtenerDatos: obtenerDatos,
  obtenerUnDato: obtenerUnDato,
  crearDato: crearDato,
  actualizarDato: actualizarDato,
  eliminarDato: eliminarDato
};

function obtenerDatos(req,res,next){
  db.any('select p.id_partido, t.nombre, p.fecha_nro from partido p inner join torneo t on p.torneo_id = t.id_torneo').then(function (data){
    res.status(200).json({
      status: 'correcto!',
      data : data,
      message: 'Consulta realizada correctamente'
    });
  })
  .catch(function(err){
    return next(err);
  });
}

function obtenerUnDato(req, res, next) {
  var datoID = parseInt(req.params.torneo_id);
  db.one("select * from torneo_equipos where torneo_id = $1", datoID)
    .then(function (data) {
      res.status(200)
        .json({
          status: 'correcto!',
          data: data,
          message: 'Obtener un dato'
        });
    })
    .catch(function (err) {
      return next(err);
    });
}

function crearDato(req, res, next) {
  req.body.torneo_id = parseInt(req.body.torneo_id);
  req.body.equipo_id = parseInt(req.body.equipo_id);
  db.none("insert into torneo(torneo_id, equipo_id)" +
      "values(${torneo_id}, ${equipo_id})",
    req.body)
    .then(function () {
      res.status(200)
        .json({
          status: 'success',
          message: 'Un equipo registrado a un torneo'
        });
    })
    .catch(function (err) {
      return next(err);
    });
}

function actualizarDato(req, res, next) {
  db.none('update torneo_equipos set torneo_id=$1, equipo_id=$2',
    [req.body.torneo_id, req.body.equipo_id])
    .then(function () {
      res.status(200)
        .json({
          status: 'success',
          message: 'Equipo modificado'
        });
    })
    .catch(function (err) {
      return next(err);
    });
}

function eliminarDato(req, res, next) {
  var datoID = parseInt(req.params.equipo_id);
  //var datoID = req.params.equipo_id;
  db.result('delete from torneo_equipos where equipo_id = $1', datoID)
    .then(function (result) {
      /* jshint ignore:start*/ 
      res.status(200)
        .json({
          status: 'success',
          message: `Equipo ${result.rowCount} borrado`
        });
      /* jshint ignore:end */
    })
    .catch(function (err) {
      return next(err);
    });
}