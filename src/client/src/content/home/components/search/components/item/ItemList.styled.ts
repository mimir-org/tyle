import styled from "styled-components/macro";

export const ItemListContainer = styled.div`
  position: relative;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.medium};

  height: 100%;

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 95%, transparent 100%);

  // Extra space for animated content
  padding-top: ${(props) => props.theme.tyle.spacing.xxs};
  padding-left: ${(props) => props.theme.tyle.spacing.small};
  padding-right: ${(props) => props.theme.tyle.spacing.small};
  margin-left: ${(props) => `-${props.theme.tyle.spacing.small}`};
  margin-right: ${(props) => `-${props.theme.tyle.spacing.small}`};

  // Hidden scrollbar
  overflow-y: scroll;
  scrollbar-width: none;
  -ms-overflow-style: none;
  ::-webkit-scrollbar {
    width: 0;
    height: 0;
  }
`;
