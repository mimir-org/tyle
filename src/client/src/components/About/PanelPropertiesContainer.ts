import { hideScrollbar } from "helpers/theme.helpers";
import styled from "styled-components/macro";

const PanelPropertiesContainer = styled.div`
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: ${(props) => props.theme.tyle.spacing.xxxl};
    padding-bottom: ${(props) => props.theme.tyle.spacing.xl};

    overflow: auto;
    ${hideScrollbar};
`;

export default PanelPropertiesContainer;
