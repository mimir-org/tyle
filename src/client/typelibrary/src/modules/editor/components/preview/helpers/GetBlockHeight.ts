const GetBlockHeight = (inputs: number, outputs: number): number => {
  let height = 115;
  if (inputs >= 4 || outputs >= 4) height = 200;
  if (inputs >= 8 || outputs >= 8) height = 285;
  if (inputs >= 12 || outputs >= 12) height = 370;
  if (inputs >= 16 || outputs >= 16) height = 420;
  return height;
};

export default GetBlockHeight;
