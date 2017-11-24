/*
import Vue from "vue"
import VueRouter from "vue-router"
import Index from "./components/changelog/Index"
import * as models from './models'

Vue.use(VueRouter)

const router = new VueRouter({
    routes: [
        { path: "/changelog", component: Index },
        { path: "/", component: Index }
    ],
    
})

const app = new Vue({
    router,
    template: "<router-view></router-view>"
    //render: (h) => h(Index),
    //template: "<Index/>",
}).$mount("#app")

*/
import * as React from "react"
import * as ReactDOM from "react-dom"
import { BrowserRouter, Route } from 'react-router-dom'
import ChangelogIndex from './containers/changelog/IndexContainer'

const App = <BrowserRouter>
    <div>
        <h1>Hello</h1>
        <Route path="/changelog" component={ChangelogIndex} />
    </div>
</BrowserRouter>

ReactDOM.render(App, document.getElementById("app"))