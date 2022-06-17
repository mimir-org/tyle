import { Link } from "react-router-dom";
import styled from "styled-components";
import { Icon } from "../../../../complib/media";

export const RegisterQrImage = styled(Icon)`
  margin: ${(props) => props.theme.tyle.spacing.l} auto;
`;

export const RegisterFinalizeLink = styled(Link)`
  align-self: end;
`;
