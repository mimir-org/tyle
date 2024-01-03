import styled from "styled-components";

export const SpinnerContainer = styled.div`
  margin: 0;
  padding: 0;
  position: fixed;
  top: 50%;
  left: 50%;
  margin-top: -50px;
  margin-left: -100px;
  width: 50px;
  height: 50px;
`;

export const SpinnerOverlay = styled.div`
  margin: 0;
  padding: 0;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 901;
`;
