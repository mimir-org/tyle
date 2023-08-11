import { RadioGroup } from "@radix-ui/react-radio-group";
import styled from "styled-components/macro";

export const TokenRadioGroupRoot = styled(RadioGroup)`
  display: flex;
  flex-wrap: wrap;
  gap: ${(props) => props.theme.mimirorg.spacing.base};
`;
