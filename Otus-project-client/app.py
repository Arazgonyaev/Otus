from flask import Flask, request, render_template
from flask_restx import Api, Resource, fields
from flask_socketio import SocketIO

app = Flask(__name__)
api = Api(app)
socketio = SocketIO(app)

game_field = {'objects': {}}

api.namespaces.clear()
game_namespace = api.namespace('game', description='Game operations')

object_model = api.model('GameObject', {
    'id': fields.String(required=True, description='Object ID'),
    'x': fields.Integer(required=True, description='X coordinate'),
    'y': fields.Integer(required=True, description='Y coordinate')
})


@game_namespace.route('/createGameField')
class CreateGameField(Resource):
    def post(self):
        game_field['objects'] = {}
        emit_game_state()
        return {'message': 'Game field created successfully'}


@game_namespace.route('/createObject')
class CreateObject(Resource):
    @api.expect(object_model)
    def post(self):
        obj = request.json
        obj_id = obj['id']
        game_field['objects'][obj_id] = obj
        emit_game_state()
        return {'message': f'Object {obj_id} created at ({obj["x"]}, {obj["y"]})'}


@game_namespace.route('/moveObject')
class MoveObject(Resource):
    @api.expect(object_model)
    def post(self):
        obj = request.json
        obj_id = obj['id']
        game_field['objects'][obj_id] = obj
        emit_game_state()
        return {'message': f'Object {obj_id} moved to ({obj["x"]}, {obj["y"]})'}


@game_namespace.route('/updateObjects')
class UpdateObjects(Resource):
    @api.expect([object_model])
    def post(self):
        game_field['objects'] = {}
        objects = request.json
        for obj in objects:
            obj_id = obj['id']
            game_field['objects'][obj_id] = obj
        emit_game_state()
        return {'message': f'Objects moved'}


def emit_game_state():
    socketio.emit('game_state', list(game_field['objects'].values()))


@app.route('/client')
def client():
    return render_template('client.html')


if __name__ == '__main__':
    socketio.run(app, debug=True, allow_unsafe_werkzeug=True)
