/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React, { Component } from 'react';
import {
    StyleSheet,
    Text,
    View,
    Image,
    Dimensions,
    TouchableOpacity
} from 'react-native';

import InputComentario from './InputComentario';
import Likes from './Likes';
import Comentario from './Comentario';

const width = Dimensions.get('screen').width;

export default class Post extends Component {



    exibeLegenda(foto) {
        if (foto.comentario === '')
            return;

        return (
            <View style={styles.comentario}>
                <Text style={styles.tituloComentario}>{foto.loginUsuario}</Text>
                <Text>{foto.comentario}</Text>
            </View>
        )
    }

    render() {
        const { foto, likesCallBack, comentarioCallBack } = this.props;

        return (
            <View>
                <View style={styles.cabecalho}>
                    <Image source={{ uri: foto.urlPerfil }}
                        style={styles.fotoDePerfil} />
                    <Text>{foto.loginUsuario}</Text>
                </View>
                <Image source={{ uri: foto.urlFoto }}
                    style={styles.foto} />
                <View style={styles.rodape}>
                    <Likes foto={foto} likesCallBack={likesCallBack} />
                    {this.exibeLegenda(foto)}
                    {foto.comentarios.map(comentario =>
                        <Comentario key={comentario.id}
                            usuario={comentario.login} texto={comentario.texto} />
                    )}
                    <InputComentario idFoto={foto.id} comentarioCallBack={comentarioCallBack} />
                </View>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    cabecalho: {
        margin: 10,
        flexDirection: 'row',
        alignItems: 'center'
    },
    fotoDePerfil: {
        width: 40,
        height: 40,
        borderRadius: 20,
        marginRight: 10
    },
    foto: {
        width: width,
        height: width
    },
    botaoDeLike: {
        height: 30,
        width: 30,
        marginBottom: 10
    },
    rodape: {
        margin: 10
    }
});