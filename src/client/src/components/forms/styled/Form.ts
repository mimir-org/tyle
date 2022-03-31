import styled from "styled-components";
import { Link } from "react-router-dom";

export const FormContainer = styled.div`
  --local-form-gap: 50px;
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: var(--local-form-gap);
  height: 100%;
  width: min(700px, 100%);
  padding: 0 min(120px, 10%);
  background-color: var(--color-grey-scale-1);
  box-shadow: 0 0 15px 10px var(--color-ui-primary-dark);
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
  font-weight: var(--font-weight-bold);
`;

export const FormHeaderText = styled.p`
  font-weight: var(--font-weight-light);
`;

export const FormErrorBanner = styled.p`
  // Remove flex gap introduced by element
  margin: calc(-1 * var(--local-form-gap)) 0;
  padding: 5px 10px;
  border: 1px solid var(--color-ui-danger-dark);
  background-color: var(--color-ui-danger);
  color: var(--color-neutral-light);
`;

export const FormInputCollection = styled.section`
  display: flex;
  flex-direction: column;
  gap: 10px;
`;

export const FormLabel = styled.label`
  font-weight: var(--font-weight-bold);
`;

export const FormError = styled.p`
  font-size: var(--font-size-standard);
  color: var(--color-ui-danger-dark);
`;

export const FormRequiredText = styled.i`
  font-size: var(--font-size-standard);
`;

export const FormActionContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 30px;
`;

export const FormButton = styled.button`
  width: 100%;
  min-height: 40px;
  border: 1px solid var(--color-ui-primary);
  border-radius: 5px;

  :active {
    border-width: 2px;
  }
`;

export const FormSecondaryActionText = styled.p`
  font-size: var(--font-size-standard);
`;

export const FormLink = styled(Link)`
  font-size: var(--font-size-standard);
`;
