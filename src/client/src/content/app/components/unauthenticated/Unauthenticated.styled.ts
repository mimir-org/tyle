import styled from "styled-components/macro";

export const UnauthenticatedLayout = styled.div`
  display: flex;
  height: 100%;

  // Generated block background
  background-color: ${(props) => props.theme.typeLibrary.color.secondary.container};
  opacity: 1;
  background-image: linear-gradient(
      30deg,
      ${(props) => props.theme.typeLibrary.color.primary.onContainer} 12%,
      transparent 12.5%,
      transparent 87%,
      ${(props) => props.theme.typeLibrary.color.primary.onContainer} 87.5%,
      ${(props) => props.theme.typeLibrary.color.primary.onContainer}
    ),
    linear-gradient(
      150deg,
      ${(props) => props.theme.typeLibrary.color.primary.onContainer} 12%,
      transparent 12.5%,
      transparent 87%,
      ${(props) => props.theme.typeLibrary.color.primary.onContainer} 87.5%,
      ${(props) => props.theme.typeLibrary.color.primary.onContainer}
    ),
    linear-gradient(
      30deg,
      ${(props) => props.theme.typeLibrary.color.secondary.base} 12%,
      transparent 12.5%,
      transparent 87%,
      ${(props) => props.theme.typeLibrary.color.secondary.base} 87.5%,
      ${(props) => props.theme.typeLibrary.color.secondary.base}
    ),
    linear-gradient(
      150deg,
      ${(props) => props.theme.typeLibrary.color.secondary.base} 12%,
      transparent 12.5%,
      transparent 87%,
      ${(props) => props.theme.typeLibrary.color.secondary.base} 87.5%,
      ${(props) => props.theme.typeLibrary.color.secondary.base}
    ),
    linear-gradient(
      60deg,
      ${(props) => props.theme.typeLibrary.color.tertiary.base} 25%,
      transparent 25.5%,
      transparent 75%,
      ${(props) => props.theme.typeLibrary.color.tertiary.base} 75%,
      ${(props) => props.theme.typeLibrary.color.tertiary.base}
    ),
    linear-gradient(
      60deg,
      ${(props) => props.theme.typeLibrary.color.tertiary.base} 25%,
      transparent 25.5%,
      transparent 75%,
      ${(props) => props.theme.typeLibrary.color.tertiary.base} 75%,
      ${(props) => props.theme.typeLibrary.color.tertiary.base}
    );
  background-size: 80px 140px;
  background-position: 0 0, 0 0, 40px 70px, 40px 70px, 0 0, 40px 70px;
`;
