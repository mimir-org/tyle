import styled from "styled-components/macro";

export const UnauthenticatedLayout = styled.div`
  display: flex;
  height: 100%;

  // Generated block background
  background-color: #3c3c4b;
  opacity: 1;
  background-image: linear-gradient(30deg, #272738 12%, transparent 12.5%, transparent 87%, #272738 87.5%, #272738),
    linear-gradient(150deg, #272738 12%, transparent 12.5%, transparent 87%, #272738 87.5%, #272738),
    linear-gradient(30deg, #272738 12%, transparent 12.5%, transparent 87%, #272738 87.5%, #272738),
    linear-gradient(150deg, #272738 12%, transparent 12.5%, transparent 87%, #272738 87.5%, #272738),
    linear-gradient(60deg, #27273877 25%, transparent 25.5%, transparent 75%, #27273877 75%, #27273877),
    linear-gradient(60deg, #27273877 25%, transparent 25.5%, transparent 75%, #27273877 75%, #27273877);
  background-size: 80px 140px;
  background-position: 0 0, 0 0, 40px 70px, 40px 70px, 0 0, 40px 70px;
`;
