import styled from "styled-components";
import { FontSize, FontWeight } from "../../../compLibrary/font";
import { Color } from "../../../compLibrary/colors";
import { Link } from "react-router-dom";

export const FormContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: 50px;
  height: 100%;
  width: min(700px, 100%);
  padding: 0 min(120px, 10%);
  background-color: hsl(210, 50%, 99%);
  box-shadow: 0 0 15px 10px hsl(240, 18%, 5%);
  overflow: auto;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  gap: 70px;
  width: 100%;
`;

export const FormHeader = styled.header``;

export const FormHeaderTitle = styled.h1`
  font-weight: ${FontWeight.Bold};
`;

export const FormHeaderText = styled.p`
  font-weight: ${FontWeight.Light};
`;

export const FormInputCollection = styled.section`
  display: flex;
  flex-direction: column;
  gap: 10px;
`;

export const FormLabel = styled.label`
  font-size: ${FontSize.Standard};
  font-weight: ${FontWeight.Bold};
`;

export const FormError = styled.p`
  font-size: ${FontSize.Standard};
  color: ${Color.RedWarning};
`;

export const FormRequiredText = styled.i`
  font-size: ${FontSize.Standard};
`;

export const FormActionContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 30px;
`;

export const FormButton = styled.button`
  width: 100%;
  min-height: 40px;
  border: 1px solid ${Color.BlueMagenta};
  border-radius: 5px;

  :active {
    border-width: 2px;
  }
`;

export const FormSecondaryActionText = styled.p`
  font-size: ${FontSize.Standard};
`;

export const FormLink = styled(Link)`
  font-size: ${FontSize.Standard};
`;
