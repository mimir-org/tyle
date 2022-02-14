import styled from "styled-components";
import { Color } from "../../compLibrary/colors";
import { FontSize, FontWeight } from "../../compLibrary/font";

export const LoginWrapper = styled.div`
  display: flex;
  height: inherit;
  background-color: ${Color.BlueMagenta};
  align-items: center;
`;

export const LoginContent = styled.div`
  display: flex;
  flex-direction: column;
  min-width: 500px;
  min-height: 500px;
  margin: 0 auto;

  img {
    align-self: center;
    margin: 24px 0px;
  }
`;

export const LoginHeader = styled.h1`
  font-size: ${FontSize.Header};
  font-weight: ${FontWeight.Bold};
  color: ${Color.White};
  padding-top: 30px;
`;

export const LoginInputLabel = styled.label`
  font-size: ${FontSize.Standard};
  color: ${Color.White};
  margin-top: 15px;
`;

export const LoginButtonsWrapper = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  margin-top: 28px;

  button {
    min-width: 180px;
    min-height: 30px;
    border: 1.5px solid ${Color.BlueMagenta};
    border-radius: 5px;
  }
`;
