import React, { Component } from 'react';
import {
    Image,
    TouchableOpacity,
    StyleSheet,
    View,
    Text
} from 'react-native';


export default class Likes extends Component {

    constructor() {
        super();
    }

    carregaIcone(likeada) {
        return likeada ?
            require('../../resources/img/s2-check.png') :
            require('../../resources/img/s2.png')
    }

    exibeLikes(likes) {
        if (likes.length <= 0)
            return;

        return (
            <Text style={styles.likes}>
                {likes.length} {likes.length > 1 ? 'curtidas' : 'curtida'}
            </Text>
        );
    }

    render() {
        const { foto, likesCallBack } = this.props;
        return (
            <View>
                <TouchableOpacity onPress={() => {
                    likesCallBack(foto.id);
                }}>
                    <Image style={styles.botaoDeLike} source={this.carregaIcone(foto.likeada)} />
                </TouchableOpacity>
                {this.exibeLikes(foto.likers)}
            </View>
        );
    }
}

const styles = StyleSheet.create({
    likes: {
        fontWeight: 'bold'
    },
    botaoDeLike: {
        height: 30,
        width: 30,
        marginBottom: 10
    }
});