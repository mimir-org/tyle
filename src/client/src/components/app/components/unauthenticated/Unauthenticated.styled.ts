import styled from "styled-components/macro";

export const UnauthenticatedLayout = styled.div`
  display: flex;
  height: 100%;

  // Generated block background
  background-color: var(--color-ui-primary-light);
  opacity: 1;
  background-image: linear-gradient(
      30deg,
      var(--color-ui-primary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--color-ui-primary) 87.5%,
      var(--color-ui-primary)
    ),
    linear-gradient(
      150deg,
      var(--color-ui-primary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--color-ui-primary) 87.5%,
      var(--color-ui-primary)
    ),
    linear-gradient(
      30deg,
      var(--color-ui-primary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--color-ui-primary) 87.5%,
      var(--color-ui-primary)
    ),
    linear-gradient(
      150deg,
      var(--color-ui-primary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--color-ui-primary) 87.5%,
      var(--color-ui-primary)
    ),
    linear-gradient(
      60deg,
      var(--color-ui-primary-alpha) 25%,
      transparent 25.5%,
      transparent 75%,
      var(--color-ui-primary-alpha) 75%,
      var(--color-ui-primary-alpha)
    ),
    linear-gradient(
      60deg,
      var(--color-ui-primary-alpha) 25%,
      transparent 25.5%,
      transparent 75%,
      var(--color-ui-primary-alpha) 75%,
      var(--color-ui-primary-alpha)
    );
  background-size: 80px 140px;
  background-position: 0 0, 0 0, 40px 70px, 40px 70px, 0 0, 40px 70px;
`;
