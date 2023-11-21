import * as Switch from "@radix-ui/react-switch";
import styled from "styled-components";

export const Wrapper = styled(Switch.Root)`
  all: unset;

  width: 42px;
  height: 25px;
  background-color: rgba(0, 0, 0, 0.1);
  border-radius: 9999px;
  position: relative;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);
  -webkit-tap-highlight-color: rgba(0, 0, 0, 0);

  &:focus {
    box-shadow: 0 0 0 2px black;
  }

  &[data-state="checked"] {
    background-color: black;
  }
`;

export const Thumb = styled(Switch.Thumb)`
  display: block;
  width: 21px;
  height: 21px;
  background-color: white;
  border-radius: 9999px;
  box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3);
  transition: transfom 100ms;
  transform: translateX(2px);
  will-change: transform;

  &[data-state="checked"] {
    transform: translateX(19px);
  }
`;
