import styled from "styled-components/macro";

export const BooleanValueFieldset = styled.fieldset`
  border: 0;
  display: flex;
  max-width: 15rem;
  justify-content: space-between;
`;

export const BooleanValueLegend = styled.legend`
  color: ${(props) => props.theme.mimirorg.color.surface.variant.on};
  font-size: 0.75rem;
  font-weight: 600;
  letter-spacing: ${1 / 24}rem;
  line-height: 1rem;
`;
