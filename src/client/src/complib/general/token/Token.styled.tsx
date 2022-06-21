import styled from "styled-components/macro";
import { Box } from "../../layouts";

export const TokenContainer = styled(Box)`
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 999px;
  width: fit-content;
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  color: ${(props) => props.theme.tyle.color.sys.surface.on};
`;
