import styled from "styled-components/macro";

const TerminalElementWrapper = styled.div`
  display: flex;
  gap: 15px;
  padding: 3px 15px 3px 15px;

  button {
    display: flex;
    align-items: center;
    max-height: 20px;
    background: transparent;
    border: none;
    cursor: pointer;
    margin: 0;
    padding: 0;
  }

  .delete-icon {
    width: 10px;
    height: 10px;
  }
`;

export default TerminalElementWrapper;
