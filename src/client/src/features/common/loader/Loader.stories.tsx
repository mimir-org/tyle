import { StoryFn } from "@storybook/react";
import { Loader } from "features/common/loader/Loader";

export default {
  title: "Features/Common/Loader",
  component: Loader,
};

const Template: StoryFn<typeof Loader> = () => <Loader />;

export const Default = Template.bind({});
