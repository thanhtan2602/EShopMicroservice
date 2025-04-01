import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import "./styles/main.scss";
import "boxicons/css/boxicons.min.css";
import { Provider } from "react-redux";
import { store } from './app/store';



const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>
);