import styled from "styled-components/macro";

export const UnitsFormWrapper = styled.form`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
`;

export const UnitRequirementFieldset = styled.fieldset`
  border: 0;
  display: flex;
  justify-content: space-between;
  max-width: 35rem;
`;

export const UnitRequirementLegend = styled.legend`
  color: ${(props) => props.theme.tyle.color.surface.variant.on};
  font-size: 0.75rem;
  font-weight: 600;
  letter-spacing: ${1 / 24}rem;
  line-height: 1rem;
`;
