export interface AnimationSystem {
  fade: Record<string, unknown>,
  scale: Record<string, unknown>,
  selectHover: Record<string, unknown>,
  from: (direction: "top" | "right" | "bottom" | "left") => Record<string, unknown>
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
  from(direction: "top" | "right" | "bottom" | "left", distance = 10) {
    const fromToMap = {
      top: {
        y: `${distance}px`
      },
      right: {
        x: `-${distance}px`
      },
      bottom: {
        y: `-${distance}px`
      },
      left: {
        x: `${distance}px`
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
  selectHover: {
    whileHover: {
      scale: 1.02
    }
  }
}
