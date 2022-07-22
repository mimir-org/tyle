import styled from "styled-components/macro";
import { getTextRole } from "../../../complib/mixins";

export const UserMenuLabel = styled.label`
  display: flex;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.base};

  ${getTextRole("label-large")}
`;
