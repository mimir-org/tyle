export interface AnimationSystem {
  fade: Record<string, unknown>,
  selectHover: Record<string, unknown>,
}

export const animation: AnimationSystem = {
  fade: {
    initial: {
      opacity: 0,
    },
    animate: {
      opacity: 1,
    },
    exit: {
      opacity: 0,
    },
  },
  selectHover: {
    whileHover: {
      scale: 1.02
    }
  }
}
