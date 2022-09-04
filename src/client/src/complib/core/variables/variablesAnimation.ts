export interface AnimationSystem {
  fade: Record<string, unknown>,
  scale: Record<string, unknown>,
  selectHover: Record<string, unknown>,
  buttonTap: Record<string, unknown>,
  checkboxTap: Record<string, unknown>,
  from: (direction: "top" | "right" | "bottom" | "left", distance?: number) => Record<string, unknown>
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
    transition: {
      type: "tween",
      ease: "easeIn"
    }
  },
  scale: {
    initial: {
      scale: 0.8,
    },
    animate: {
      scale: 1,
    },
    exit: {
      scale: 0.8,
    },
  },
  selectHover: {
    whileHover: {
      scale: 1.02
    }
  },
  buttonTap: {
    whileTap: {
      scale: 0.95
    }
  },
  checkboxTap: {
    whileTap: {
      scale: 0.8
    }
  },
  from(direction: "top" | "right" | "bottom" | "left", distance = 10) {
    const fromToMap = {
      top: {
        y: `-${distance}px`
      },
      right: {
        x: `${distance}px`
      },
      bottom: {
        y: `${distance}px`
      },
      left: {
        x: `-${distance}px`
      }
    }

    return {
      initial: fromToMap[direction],
      animate: {
        x: 0,
        y: 0,
      },
      exit: fromToMap[direction]
    }
  },
}
