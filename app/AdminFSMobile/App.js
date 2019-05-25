/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React, { Component } from 'react';
import {
  Platform,
  StyleSheet,
  Text,
  View,
  Image,
  Dimensions,
  FlatList
} from 'react-native';

import Post from './src/components/Post';

const width = Dimensions.get('screen').width;

export default class App extends Component {

  constructor() {
    super();
    this.state = {
      fotos: []
    }
  }

  componentDidMount() {
    fetch('https://instalura-api.herokuapp.com/api/public/fotos/rafael')
      .then(res => res.json())
      .then(json => this.setState({ fotos: json }))
  }

  adicionaComentario(idFoto, valorComentario, inputComentario) {
    if (valorComentario === '')
      return;

    const foto = this.state.fotos.find(foto => foto.id === idFoto);

    const novaLista = [
      ...foto.comentarios,
      {
        id: valorComentario,
        login: 'meuUsuario',
        texto: valorComentario
      }
    ];

    const fotoAtualizada = {
      ...foto,
      comentarios: novaLista
    }

    const fotos = this.state.fotos
      .map(foto => foto.id === fotoAtualizada.id ? fotoAtualizada : foto);

    this.setState({ fotos });
    inputComentario.clear();
  }

  like(idFoto) {
    let novosLikes = [];
    const foto = this.state.fotos.find(foto => foto.id === idFoto);

    if (!foto.likeada) {
      novosLikes = [
        ...foto.likers,
        { loginUsuario: 'nomeUsuario' }
      ];
    }
    else {
      novosLikes = foto.likers.filter(liker => {
        return liker.loginUsuario !== 'nomeUsuario';
      });
    }

    const fotoAtualizada = {
      ...foto,
      likeada: !foto.likeada,
      likers: novosLikes
    };

    const fotos = this.state.fotos
      .map(foto => foto.id === fotoAtualizada.id ? fotoAtualizada : foto);

    this.setState({ fotos });
  }

  render() {
    return (
      <FlatList style={styles.container}
        data={this.state.fotos}
        keyExtractor={item => String(item.id)}
        renderItem={({ item }) =>
          <Post foto={item} likesCallBack={this.like.bind(this)}
            comentarioCallBack={this.adicionaComentario.bind(this)} />
        }

      />
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
  }
});