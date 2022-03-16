import styled from "styled-components";
import { Icon } from "../../../../compLibrary/icon";
import { Link } from "react-router-dom";

export const RegisterFinalizeContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 30px;
`;

export const RegisterFinalizeSection = styled.section`
  display: flex;
  flex-direction: column;
  padding-bottom: 30px;

  :first-child {
    border-bottom: 2px solid hsla(240, 18%, 19%, 0.3);
  }
`;

export const RegisterFinalizeHeader = styled.h1``;

export const RegisterFinalizeText = styled.p``;

export const RegisterQrImage = styled(Icon)`
  margin: 20px auto;
`;

export const RegisterFinalizeLink = styled(Link)`
  align-self: end;
`;
