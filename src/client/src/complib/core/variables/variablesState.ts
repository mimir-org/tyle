import { css } from "styled-components/macro";

interface State {
  opacity: number
}

export interface StateSystem {
  hover: State,
  focus: State,
  pressed: State,
  dragged: State,
  enabled: State,
  disabled: {
    container: State,
    content: State
  }
}

export const state: StateSystem = {
  hover: {
    opacity: 0.08
  },
  focus: {
    opacity: 0.12
  },
  pressed: {
    opacity: 0.12
  },
  dragged: {
    opacity: 0.16
  },
  enabled: {
    opacity: 1
  },
  disabled: {
    container: {
      opacity: 0.12
    },
    content: {
      opacity: 0.38
    }
  }
}

export const variablesState = css`
  :root {
    --tl-sys-state-hover-opactiy: ${state.hover.opacity};
    --tl-sys-state-focus-opactiy: ${state.focus.opacity};
    --tl-sys-state-pressed-opactiy: ${state.pressed.opacity};
    --tl-sys-state-dragged-opactiy: ${state.dragged.opacity};
  }
`;