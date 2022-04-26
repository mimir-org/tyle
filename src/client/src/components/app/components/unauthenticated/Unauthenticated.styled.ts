import styled from "styled-components/macro";

export const UnauthenticatedLayout = styled.div`
  display: flex;
  height: 100%;

  // Generated block background
  background-color: var(--tl-sys-color-tertiary-container);
  opacity: 1;
  background-image: linear-gradient(
      30deg,
      var(--tl-sys-color-inverse-primary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--tl-sys-color-inverse-primary) 87.5%,
      var(--tl-sys-color-inverse-primary)
    ),
    linear-gradient(
      150deg,
      var(--tl-sys-color-inverse-primary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--tl-sys-color-inverse-primary) 87.5%,
      var(--tl-sys-color-inverse-primary)
    ),
    linear-gradient(
      30deg,
      var(--tl-sys-color-secondary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--tl-sys-color-secondary) 87.5%,
      var(--tl-sys-color-secondary)
    ),
    linear-gradient(
      150deg,
      var(--tl-sys-color-secondary) 12%,
      transparent 12.5%,
      transparent 87%,
      var(--tl-sys-color-secondary) 87.5%,
      var(--tl-sys-color-secondary)
    ),
    linear-gradient(
      60deg,
      var(--tl-sys-color-tertiary) 25%,
      transparent 25.5%,
      transparent 75%,
      var(--tl-sys-color-tertiary) 75%,
      var(--tl-sys-color-tertiary)
    ),
    linear-gradient(
      60deg,
      var(--tl-sys-color-tertiary) 25%,
      transparent 25.5%,
      transparent 75%,
      var(--tl-sys-color-tertiary) 75%,
      var(--tl-sys-color-tertiary)
    );
  background-size: 80px 140px;
  background-position: 0 0, 0 0, 40px 70px, 40px 70px, 0 0, 40px 70px;
`;
