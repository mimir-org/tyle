import styled from "styled-components/macro";

export const UnauthenticatedLayout = styled.div`
  display: flex;
  height: 100%;

  // Generated block background
  background-color: ${(props) => props.theme.tyle.color.ref.primary[95]};
  opacity: 1;
  background-image: linear-gradient(
      30deg,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 12%,
      transparent 12.5%,
      transparent 70%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 87.5%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]}
    ),
    linear-gradient(
      150deg,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 12%,
      transparent 12.5%,
      transparent 87%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 87.5%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]}
    ),
    linear-gradient(
      30deg,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 12%,
      transparent 12.5%,
      transparent 70%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 87.5%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]}
    ),
    linear-gradient(
      150deg,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 12%,
      transparent 12.5%,
      transparent 87%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]} 87.5%,
      ${(props) => props.theme.tyle.color.ref.neutral[20]}
    ),
    linear-gradient(
      60deg,
      ${(props) => props.theme.tyle.color.ref.neutral[40]} 25%,
      transparent 25.5%,
      transparent 30%,
      ${(props) => props.theme.tyle.color.ref.neutral[40]} 75%,
      ${(props) => props.theme.tyle.color.ref.neutral[40]}
    ),
    linear-gradient(
      60deg,
      ${(props) => props.theme.tyle.color.ref.neutral[40]} 25%,
      transparent 25.5%,
      transparent 75%,
      ${(props) => props.theme.tyle.color.ref.neutral[40]} 75%,
      ${(props) => props.theme.tyle.color.ref.neutral[40]}
    );
  background-size: 80px 140px;
  background-position: 0 0, 0 0, 40px 70px, 40px 70px, 0 0, 40px 70px;
`;
