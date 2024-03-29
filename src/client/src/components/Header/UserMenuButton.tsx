import Button from "components/Button";
import styled from "styled-components/macro";

const UserMenuButton = styled(Button).attrs(() => ({
  variant: "text",
  textVariant: "label-large",
  iconPlacement: "left",
}))`
  width: 100%;
  justify-content: start;
`;

export default UserMenuButton;
