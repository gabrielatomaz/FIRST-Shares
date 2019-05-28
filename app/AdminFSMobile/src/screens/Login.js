import React, { Component } from 'react';
import {
    StyleSheet,
    Text,
    View,
    Image,
    Dimensions,
    TextInput,
    Button,
    AsyncStorage
} from 'react-native';

const width = Dimensions.get('screen').width;

export default class Login extends Component {

    constructor() {
        super();
        this.state = {
            usuario: '',
            senha: '',
            mensagem: ''
        }
    }

    efetuaLogin() {
        const uri ="https://localhost:44324/api/usuario/retornarusuarioautenticado/"
            + this.state.usuario + "/" + this.state.senha;
        console.warn(uri);
        fetch(uri)
            .then(resp => {
                if (resp.ok)
                    return resp.text();
                    
                throw new Error("Não foi possível efetuar o login.");
            })
            .then(token => {
                AsyncStorage.setItem('userName', this.state.usuario);

                return AsyncStorage.getItem('userName');
            }).catch(e => this.setState({ mensagem: e.message }));
    }

    render() {
        return (
            <View style={styles.container}>
                <Image style={styles.logo} source={require('../../resources/img/logo.png')}></Image>
                <View style={styles.form}>
                    <TextInput style={styles.input} placeholder="Usuário..."
                        onChangeText={texto => this.setState({ usuario: texto })}
                        autoCapitalize="none"></TextInput>
                    <TextInput style={styles.input} placeholder="Senha..."
                        onChangeText={texto => this.setState({ senha: texto })}
                        autoCapitalize="none" secureTextEntry={true}></TextInput>
                    <Button style={styles.btn} color="#316B5A" title="Entrar"
                        onPress={this.efetuaLogin.bind(this)}></Button>
                </View>
                <Text style={styles.mensagem}>
                    {this.state.mensagem}
                </Text>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center'
    },
    form: {
        width: width * 0.6
    },
    input: {
        height: 40,
        borderBottomWidth: 1,
        borderBottomColor: '#ddd'
    },
    logo: {
        height: 140,
        width: 110,
        marginBottom: 20
    },
    btn: {
        marginTop: 20
    },
    mensagem: {
        marginTop: 20,
        color: "#e74c3c"
    }
});