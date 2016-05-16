/*
 This file is part of the Structure SDK.
 Copyright © 2015 Occipital, Inc. All rights reserved.
 http://structure.io
*/

#import <Structure/Structure.h>

@interface StructureAR : NSObject  <AVCaptureVideoDataOutputSampleBufferDelegate, STSensorControllerDelegate>
{
}

+(StructureAR*)sharedStructureAR;
@end
