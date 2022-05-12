import styled from "styled-components";
import { Icon } from "../../../../complib/media";
import { Link } from "react-router-dom";

export const RegisterQrImage = styled(Icon)`
  margin: ${(props) => props.theme.tyle.spacing.large} auto;
`;

export const RegisterFinalizeLink = styled(Link)`
  align-self: end;
`;
