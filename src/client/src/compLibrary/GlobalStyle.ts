import { createGlobalStyle } from "styled-components/macro";
import { FontType } from "./font";

const GlobalStyle = createGlobalStyle`
  // CSS RESET (https://www.joshwcomeau.com/css/custom-css-reset/)
  *, *::before, *::after {
    box-sizing: border-box;
  }

  * {
    margin: 0;
  }

  html, body {
    height: 100%;
  }

  body {
    line-height: 1.5;
    -webkit-font-smoothing: antialiased;
  }

  img, picture, video, canvas, svg {
    display: block;
    max-width: 100%;
  }

  input, button, textarea, select {
    font: inherit;
  }

  p, h1, h2, h3, h4, h5, h6 {
    overflow-wrap: break-word;
  }

  #root, #__next {
    isolation: isolate;
    height: 100%;
  }
  
  // APPLICATION SPECIFIC GLOBALS
  html, body {
    font-family: ${FontType.Standard};
  };

  ::-webkit-scrollbar {
    width: 14px;
    height: 18px;
  }
  
  ::-webkit-scrollbar-thumb {
    border: 4px solid rgba(0, 0, 0, 0);
    background-clip: padding-box;
    border-radius: 7px;
    background-color: #C4C4C4;
  }

  ::-webkit-scrollbar-track {
    background-color: transparent;
  }
`;

export default GlobalStyle;
