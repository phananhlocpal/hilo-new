import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import { legacy_createStore as createStore, applyMiddleware} from 'redux';
import { Provider } from "react-redux";
import { allReducers } from "./redux/reducers/index.js"
<<<<<<< HEAD
import { thunk }  from 'redux-thunk';
=======
import { thunk } from 'redux-thunk';
>>>>>>> 960a83c (commit)

const store = createStore(allReducers, applyMiddleware(thunk));

ReactDOM.createRoot(document.getElementById('root')).render(
  <Provider store={store}>
    <React.StrictMode>
      <App />
<<<<<<< HEAD
    </React.StrictMode>
=======
    </React.StrictMode>,
>>>>>>> 960a83c (commit)
  </Provider>

)