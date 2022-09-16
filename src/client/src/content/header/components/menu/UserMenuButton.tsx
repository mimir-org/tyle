import styled from "styled-components/macro";
import { Button } from "../../../../complib/buttons";

export const UserMenuButton = styled(Button).attrs(() => ({
  variant: "text",
  textVariant: "label-large",
  iconPlacement: "left",
}))`
  width: 100%;
  justify-content: start;
`;
